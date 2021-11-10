import "./Row.css";

const Row = (props) => {
  return (
    <tr>
      {props.data.map((cell, index) => (
        <td className={index === 0 ? "identityCell" : ""} key={index}>
          {cell}
        </td>
      ))}
      <td>
        <button className="actionBtn actionBtn--red" >Delete</button>
        <button onClick={props.changeState.bind(null,props.data[0])} className="actionBtn" >Update</button>
      </td>
    </tr>
  );
};

export default Row;
