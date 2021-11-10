import { useEffect, useState } from "react";
import Row from "./Row";
import "./Table.css";

const Table = ({ title, url }) => {
  const [rows, setRows] = useState([["placeholder"]]);
  const [page, setPage] = useState(1);
  const [columns, setColumns] = useState([]);
  const [pages, setPages] = useState(0);
  const [updatingId, setUpdatingId] = useState(null);
  const [inputs, setInputs] = useState({});

    const changeStateHandler = (id) =>{
        setUpdatingId(id);
    }

  useEffect(() => {
    (async () => {
      const res = await fetch(`${url}/paged/5/${page}`);
      const jsonData = await res.json();
      setPages(jsonData.pages);
      if (page !== jsonData.page) {
        setPage(jsonData.page);
      }
      let dataColumns = [];
      for (let column in jsonData.data[0]) {
        dataColumns.push(<th key={column}>{column}</th>);
      }

      let inputsObject = {};
      let inputsArray = Object.keys(jsonData.data[0]).slice(1);
      inputsArray.forEach(input =>{
          inputsObject[input] = "";
      })
      setInputs(inputsObject);

      setColumns(dataColumns);

      let arrayRows = [];

      for (let row in jsonData.data) {
        arrayRows.push(Object.values(jsonData.data[row]));
      }
      setRows(arrayRows);
    })();
  }, [page]);

  return (
    <>
      <div className="tableContainer">
        <div>
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
                <Row changeState={changeStateHandler} key={index} data={row} />
              ))}
            </tbody>
          </table>
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
        <div className="tableActions">
          <h3>
            {updatingId
              ? `Update ${title} with id ${updatingId}`
              : `Add ${title}`}
          </h3>
          <form className="form">
            {Object.keys(inputs).map((input,index) => (
                <>
              <label className="inputLabel" htmlFor={input}>
                {input + ": "}
              </label>
              <input placeholder={"ex. "+rows[0][index+1]} className="input" type="text" />
              </>
            ))}
            <div className="formButtons">
            {
                updatingId ?
                <>
                    <button type="submit" className="actionBtn actionBtn--green" >Update {title}</button>
                    <button onClick={setUpdatingId.bind(null,null)} type="button" className="actionBtn actionBtn--red" >Cancel</button>
                </>
                :  <button type="submit" className="actionBtn actionBtn--blue" >+ Add {title}</button>
            }
            </div>
          </form>
        </div>
      </div>
    </>
  );
};

export default Table;
