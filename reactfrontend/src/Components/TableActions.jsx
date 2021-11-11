import AddCircleIcon from "@mui/icons-material/AddCircle";
import UpgradeIcon from "@mui/icons-material/Upgrade";
import CancelIcon from "@mui/icons-material/Cancel";
import { Fragment } from "react";

const TableActions = ({
  updatingId,
  title,
  inputs,
  placeholderModel,
  cancelUpdatingState,
  onInputChange,
  onAdd,
  error,
  onUpdate,
}) => {
  return (
    <div className="tableActions">
      <h3>
        {updatingId ? `Update ${title} with id ${updatingId}` : `Add ${title}`}
      </h3>
      <form className="form">
        {error && <p className="error">{error}</p>}
        {Object.keys(inputs).map((input, index) => (
          <Fragment key={index}>
            <label className="inputLabel" htmlFor={input}>
              {input + ": "}
            </label>
            <input
              placeholder={"ex. " + placeholderModel[index + 1]}
              className="input"
              name={input}
              type="text"
              value={inputs[input]}
              onChange={onInputChange(input)}
            />
          </Fragment>
        ))}
        <div className="formButtons">
          {updatingId ? (
            <>
              <button
                onClick={onUpdate}
                type="submit"
                className="actionBtn actionBtn--green"
              >
                Update {title} <UpgradeIcon />
              </button>
              <button
                onClick={cancelUpdatingState}
                type="button"
                className="actionBtn actionBtn--red"
              >
                Cancel <CancelIcon />
              </button>
            </>
          ) : (
            <button
              onClick={onAdd}
              type="submit"
              className="actionBtn actionBtn--blue"
            >
              Add {title} <AddCircleIcon />
            </button>
          )}
        </div>
      </form>
    </div>
  );
};

export default TableActions;
