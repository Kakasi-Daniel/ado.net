import { FC} from "react";
import MovieModel from "./Models/MovieModel";
import Table from "./Components/Table";
import ActorModel from "./Models/ActorModel";
import RoleModel from "./Models/RoleModel";
import RatingModel from "./Models/RatingModel";

const App: FC = () => {

  return (
    <div>
      <Table modelType={new MovieModel()} title="Movie" />
      <Table modelType={new ActorModel()} title="Actor" />
      <Table modelType={new RoleModel()} title="Role" />
      <Table modelType={new RatingModel()} title="Rating" />
    </div>
  );
};

export default App;
