import { Fragment, useEffect, useState } from "react";
import ActorModel from "../Models/ActorModel";
import MovieModel from "../Models/MovieModel";
import RatingModel from "../Models/RatingModel";
import RoleModel from "../Models/RoleModel";
import getTable, {
  addRow,
  deleteRow,
  getRow,
  updateRow,
} from "../Services/HttpServices";
import "./Table.css";
import { GoDiffAdded, FiTrash,GoDiffRenamed,MdOutlineCancelPresentation, GiUpgrade } from "react-icons/all";

const Table = <
  T extends MovieModel | ActorModel | RatingModel | RoleModel
>(props: {
  modelType: T;
  title: string;
}) => {
  const [rows, setRows] = useState<string[][]>([[]]);
  const [page, setPage] = useState(1);
  const [columns, setColumns] = useState<JSX.Element[]>([]);
  const [pages, setPages] = useState(0);
  const [updatingId, setUpdatingId] = useState("");
  const [inputs, setInputs] = useState<T>(props.modelType);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState("");
  const [pageHeight, setPageHeight] = useState("5");

  const inputChangerGenerator = (
    input: string
  ): React.ChangeEventHandler<HTMLInputElement> => {
    return (e) => {
      setInputs({ ...inputs, [input]: e.target.value });
    };
  };

  const changePageHeightHandler: React.ChangeEventHandler<HTMLInputElement> = (
    e
  ) => {
    const newPageHeight = +e.target.value;
    if (newPageHeight >= 3 && newPageHeight <= 10) {
      setPageHeight(newPageHeight.toString());
    }
  };

  const updateStateHandler = async (id: string) => {
    setUpdatingId(id);
    if(error) setError("");

    const data = await getRow(props.title, id);

    const updatingInputs = { ...props.modelType };

    for (var inputUpdated in updatingInputs) {
      updatingInputs[inputUpdated] = data[inputUpdated];
    }

    console.log(data);
    console.log(updatingInputs);

    setInputs(updatingInputs);
  };

  const addHandler: React.MouseEventHandler<HTMLButtonElement> = async (e) => {
    e.preventDefault();

    const audio = new Audio("https://media1.vocaroo.com/mp3/1nQMMVelCgae");
    let blankFields = false;

    for (let inp in inputs) {
      if (!inputs[inp]) {
        blankFields = true;
        break;
      }
    }
    if (!blankFields) {
      try {
        const res = await addRow(props.title, inputs);
        if (res.status >= 300) {
          audio.play();
          setError("Error, check fields validity.");
        } else {
          setInputs(props.modelType);
          setError("");
          setSubmitting(true);
        }
      } catch (e) {
        audio.play();
        setError("Error, check fields validity.");
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
        const res = await updateRow(props.title, inputs, updatingId);
        if (res.status > 299) {
          setError("Error, check fields validity.");
        } else {
          cancelUpdateHandler();
          setError("");
          setSubmitting(true);
        }
      } catch (e) {
        setError(`Error updating the ${props.title} try again later.`);
      }
    } else {
      setError("All fields are mandatory!");
    }
  };

  const deleteHandler = async (id: string) => {
    const confirmResult = window.confirm(
      `${props.title} with id ${id} will be permanently deleted!`
    );

    if (confirmResult) {
      await deleteRow(props.title, id);
      setSubmitting(true);
      if (updatingId === id) {
        cancelUpdateHandler();
      }
    }
  };

  const cancelUpdateHandler = () => {
    if(error) setError("");
    setUpdatingId("");
    setInputs(props.modelType);
  };

  useEffect(() => {
    (async () => {
      setSubmitting(false);
      const tableData = await getTable(
        props.title,
        pageHeight,
        page.toString()
      );

      setPages(tableData.pages);
      if (page !== tableData.page) {
        setPage(tableData.page);
      }
      let dataColumns: JSX.Element[] = [];
      for (let column in tableData.data[0]) {
        dataColumns.push(<th key={column}>{column}</th>);
      }

      setColumns(dataColumns);

      let arrayRows: string[][] = [];

      for (let row in tableData.data) {
        arrayRows.push(Object.values(tableData.data[row]));
      }
      setRows(arrayRows);
    })();
  }, [page, submitting, props.title, pageHeight]);

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
                        onClick={deleteHandler.bind(null, row[0])}
                        className="actionBtn actionBtn--red"
                      >
                        Delete
                        &nbsp;
                        <FiTrash/>
                      </button>
                      <button
                        onClick={updateStateHandler.bind(null, row[0])}
                        className="actionBtn"
                      >
                        Update
                        &nbsp;
                        <GoDiffRenamed/>
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
          <div className="pageControls">
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
            <input
              className="pageHeight"
              type="number"
              onChange={changePageHeightHandler}
              value={pageHeight}
            />
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
            {
              (Object.keys(inputs) as Array<keyof T>).map((input, index) => (
              <Fragment key={index}>
                <label className="inputLabel" htmlFor={input.toString()}>
                  {input + ": "}
                </label>
                <input
                  placeholder={`ex. ${rows[0][index + 1]}`}
                  className="input"
                  name={input.toString()}
                  type="text"
                  value={`${inputs[input]}`}
                  onChange={inputChangerGenerator(input.toString())}
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
                    &nbsp;
                    <GiUpgrade />
                  </button>
                  <button
                    onClick={cancelUpdateHandler}
                    type="button"
                    className="actionBtn actionBtn--red"
                  >
                    Cancel
                    &nbsp;
                    <MdOutlineCancelPresentation/>
                  </button>
                </>
              ) : (
                <button
                  onClick={addHandler}
                  type="submit"
                  className="actionBtn actionBtn--blue"
                >
                  Add {props.title}
                  &nbsp;
                  <GoDiffAdded />
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
