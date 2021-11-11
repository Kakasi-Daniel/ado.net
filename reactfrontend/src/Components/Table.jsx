import { useEffect, useState } from "react";
import Row from "./Row";
import "./Table.css";
import TableActions from "./TableActions";

const ignoredInputs = ["id", "movieName", "actorName"];

const Table = ({ title }) => {
  const [rows, setRows] = useState([[]]);
  const [page, setPage] = useState(1);
  const [columns, setColumns] = useState([]);
  const [pages, setPages] = useState(0);
  const [updatingId, setUpdatingId] = useState(null);
  const [inputs, setInputs] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState(false);

  const changeStateHandler = async (id) => {
    setUpdatingId(id);
    const res = await fetch(`https://localhost:44333/api/${title}s/${id}`);
    const data = await res.json();
    const updatingInputs = {};
    for (let inpData of Object.keys(data)) {
      if (!ignoredInputs.includes(inpData))
        updatingInputs[inpData] = data[inpData];
    }

    setInputs(updatingInputs);
  };

  const cancelUpdatingStateHandler = () => {
    setUpdatingId(null);
    const restoredInputs = {};
    for (let inpDat of Object.keys(inputs)) {
      restoredInputs[inpDat] = "";
    }

    setInputs(restoredInputs);
  };

  const inputChangeHandler = (input) => {
    return (e) => {
      setInputs({ ...inputs, [input]: e.target.value });
    };
  };

  const addHandler = async (e) => {
    e.preventDefault();
    let blankFields = false;
    for (let inp in inputs) {
      if (inp.trim().length === 0) {
        blankFields = true;
        break;
      }
    }
    if (!blankFields) {
      try {
        const res = await fetch(`https://localhost:44333/api/${title}s`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(inputs),
        });
        if (res.status > 299) {
          setError("Error, check fields validity.");
        } else {
          const restoredInputs = {};
          for (let inpDat of Object.keys(inputs)) {
            restoredInputs[inpDat] = "";
          }

          setInputs(restoredInputs);
          setError(false);
          setSubmitting(true);
        }
      } catch (e) {
        setError(`Error adding the ${title} try again later.`);
      }
    } else {
      setError("All fields are mandatory!");
    }
  };

  const deleteHandler = async (id) => {
    await fetch(`https://localhost:44333/api/${title}s/${id}`, {
      method: "DELETE",
    });
    setSubmitting(true);
    cancelUpdatingStateHandler();
  };

  const updateHandler = async (e) => {
    e.preventDefault();
    let blankFields = false;
    for (let inp in inputs) {
      if (inp.trim().length === 0) {
        blankFields = true;
        break;
      }
    }
    if (!blankFields) {
      try {
        const res = await fetch(
          `https://localhost:44333/api/${title}s/${updatingId}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(inputs),
          }
        );
        if (res.status > 299) {
          setError("Error, check fields validity.");
        } else {
          cancelUpdatingStateHandler();
          setError(false);
          setSubmitting(true);
        }
      } catch (e) {
        setError(`Error adding the ${title} try again later.`);
      }
    } else {
      setError("All fields are mandatory!");
    }
  };

  useEffect(() => {
    (async () => {
      setSubmitting(false);
      const res = await fetch(
        `https://localhost:44333/api/${title}s/paged/5/${page}`
      );
      const jsonData = await res.json();
      setPages(jsonData.pages);
      if (page !== jsonData.page) {
        setPage(jsonData.page);
      }
      let dataColumns = [];
      for (let column in jsonData.data[0]) {
        dataColumns.push(<th key={column}>{column}</th>);
      }

      if (!inputs) {
        let inputsObject = {};
        let inputsArray = Object.keys(jsonData.data[0]);
        inputsArray.forEach((input) => {
          if (!ignoredInputs.includes(input)) inputsObject[input] = "";
        });
        setInputs(inputsObject);
      }

      setColumns(dataColumns);

      let arrayRows = [];

      for (let row in jsonData.data) {
        arrayRows.push(Object.values(jsonData.data[row]));
      }
      setRows(arrayRows);
    })();
  }, [page, submitting, title, inputs]);

  return (
    <>
      <div className="tableContainer">
        <div>
          <div className="table">
            <h2>{title}s Table</h2>
            <table>
              <thead>
                <tr>
                  {columns}
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {rows?.map((row, index) => (
                  <Row
                    changeState={changeStateHandler}
                    key={index}
                    data={row}
                    onDelete={deleteHandler}
                  />
                ))}
              </tbody>
            </table>
          </div>
          <div className="pageButtons">
            {new Array(pages).fill().map((_, index) => (
              <button
                className={
                  index + 1 === page ? "current pageButton" : "pageButton"
                }
                key={index}
                onClick={setPage.bind(null, index + 1)}
              >
                {index + 1}
              </button>
            ))}
          </div>
        </div>
        <TableActions
          error={error}
          placeholderModel={rows[0]}
          title={title}
          updatingId={updatingId}
          inputs={inputs}
          cancelUpdatingState={cancelUpdatingStateHandler}
          onInputChange={inputChangeHandler}
          onAdd={addHandler}
          onUpdate={updateHandler}
        />
      </div>
    </>
  );
};

export default Table;
