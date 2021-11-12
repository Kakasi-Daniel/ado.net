import { Fragment, useEffect, useState } from "react";
import "./Table.css";

const ignoredInputs = ["id","movieName","actorName"]

const Table = <T extends object>(props: { modelType: T; title: string }) => {
  const [rows, setRows] = useState<string[][]>([[]]);
  const [page, setPage] = useState(1);
  const [columns, setColumns] = useState<JSX.Element[]>([]);
  const [pages, setPages] = useState(0);
  const [updatingId, setUpdatingId] = useState("");
  const [inputs, setInputs] = useState<T>(props.modelType);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");

  const inputChangerGenerator = (
    input: string
  ): React.ChangeEventHandler<HTMLInputElement> => {
    return (e) => {
      setInputs({ ...inputs, [input]: e.target.value });
    };
  };

  const updateStateHandler = async (id: string) => {
    setUpdatingId(id);
    const res = await fetch(
      `https://localhost:44333/api/${props.title}s/${id}`
    );
    const data = await res.json();

    // const filteredData:T = {};
    // for(let key of data){
    //   if(!ignoredInputs.includes(key)) filteredData[key] = data[key];
    // }

    // const updatingInputs:T = {...inputs}

    // for(var key:string in data){
    //   updatingInputs[key] = data[key];
    // }

    

    setInputs({...data});
  };

  const addHandler: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();

    const audio = new Audio("https://media1.vocaroo.com/mp3/1nQMMVelCgae");

    let blankFields = false;
    for (let inp in inputs) {
      if (inp.trim().length === 0) {
        blankFields = true;
        break;
      }
    }
    if (!blankFields) {
      try {
        const res = await fetch(`https://localhost:44333/api/${props.title}s`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(inputs),
        });
        if (res.status > 299) {
          audio.play();
          setError("Error, check fields validity.");
        } else {
          // const restoredInputs = inputs;
          // for (let inpDat:(keyof T) in Object.keys(restoredInputs)) {
          //   restoredInputs[inpDat] = "";
          // }
          // setInputs(restoredInputs);

          setInputs(props.modelType);

          setError("");
          setSubmitting(true);
        }
      } catch (e) {
        audio.play();
        setError(`Error adding the ${props.title} try again later.`);
      }
    } else {
      audio.play();
      setError("All fields are mandatory!");
    }
  };

  const updateHandler: React.MouseEventHandler<HTMLButtonElement> = async (
    e
  ) => {
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
          `https://localhost:44333/api/${props.title}s/${updatingId}`,
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
          cancelUpdateHandler();
          setError("");
          setSubmitting(true);
        }
      } catch (e) {
        setError(`Error adding the ${props.title} try again later.`);
      }
    } else {
      setError("All fields are mandatory!");
    }
  };

  const cancelUpdateHandler = () => {
    setUpdatingId("");
    // const restoredInputs = {};
    // for (let inpDat of Object.keys(inputs)) {
    //   restoredInputs[inpDat] = "";
    // }

    setInputs(props.modelType);
  };

  useEffect(() => {
    (async () => {
      setSubmitting(false);
      const res = await fetch(
        `https://localhost:44333/api/${props.title}s/paged/5/${page}`
      );
      const jsonData = await res.json();
      setPages(jsonData.pages);
      if (page !== jsonData.page) {
        setPage(jsonData.page);
      }
      let dataColumns: JSX.Element[] = [];
      for (let column in jsonData.data[0]) {
        dataColumns.push(<th key={column}>{column}</th>);
      }

      setColumns(dataColumns);

      let arrayRows: string[][] = [];

      for (let row in jsonData.data) {
        arrayRows.push(Object.values(jsonData.data[row]));
      }
      setRows(arrayRows);
    })();
  }, [page, submitting, props.title]);

  return (
    <>
      <div className="tableContainer">
        <div>
          <div className="table">
            <h2>{props.title}s Table</h2>
            <table>
              <thead>
                <tr>
                  {columns}
                  <th>Actions</th>
                </tr>
              </thead>
              <tbody>
                {rows?.map((row, indexRow) => (
                  <tr key={indexRow}>
                    {row.map((cell, indexCell) => (
                      <td
                        className={indexCell === 0 ? "identityCell" : ""}
                        key={indexCell}
                      >
                        {cell}
                      </td>
                    ))}
                    <td className="actionButtonsRow">
                      <button
                        onClick={cancelUpdateHandler}
                        className="actionBtn actionBtn--red"
                      >
                        Delete
                      </button>
                      <button
                        onClick={updateStateHandler.bind(null, row[0])}
                        className="actionBtn"
                      >
                        Update
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="pageButtons">
            {new Array(pages).fill(null).map((_, index) => (
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
        <div className="tableActions">
          <h3>
            {updatingId
              ? `Update ${props.title} with id ${updatingId}`
              : `Add ${props.title}`}
          </h3>
          <form className="form">
            {error && <p className="error">{error}</p>}
            {Object.keys(inputs).map((input, index) => (
              <Fragment key={index}>
                <label className="inputLabel" htmlFor={input}>
                  {input + ": "}
                </label>
                <input
                  placeholder={`ex. ${rows[0][index + 1]}`}
                  className="input"
                  name={input}
                  type="text"
                  value={Object.values(inputs)[index]}
                  onChange={inputChangerGenerator(input)}
                />
              </Fragment>
            ))}
            <div className="formButtons">
              {updatingId ? (
                <>
                  <button
                    onClick={updateHandler}
                    type="submit"
                    className="actionBtn actionBtn--green"
                  >
                    Update {props.title}
                  </button>
                  <button
                    onClick={cancelUpdateHandler}
                    type="button"
                    className="actionBtn actionBtn--red"
                  >
                    Cancel
                  </button>
                </>
              ) : (
                <button
                  onClick={addHandler}
                  type="submit"
                  className="actionBtn actionBtn--blue"
                >
                  Add {props.title}
                </button>
              )}
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Table;
