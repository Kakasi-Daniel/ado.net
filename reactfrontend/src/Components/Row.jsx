import "./Row.css";
import DeleteIcon from "@mui/icons-material/Delete";
import UpgradeIcon from "@mui/icons-material/Upgrade";

const Row = (props) => {
  return (
    <tr>
      {props.data.map((cell, index) => (
        <td className={index === 0 ? "identityCell" : ""} key={index}>
          {cell}
        </td>
      ))}
      <td className="actionButtonsRow">
        <button
          onClick={props.onDelete.bind(null, props.data[0])}
          className="actionBtn actionBtn--red"
        >
          Delete <DeleteIcon />
        </button>
        <button
          onClick={props.changeState.bind(null, props.data[0])}
          className="actionBtn"
        >
          Update <UpgradeIcon />
        </button>
      </td>
    </tr>
  );
};

export default Row;
