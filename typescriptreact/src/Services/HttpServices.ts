import axios from "axios";
import ActorModel from "../Models/ActorModel";
import MovieModel from "../Models/MovieModel";
import RatingModel from "../Models/RatingModel";
import RoleModel from "../Models/RoleModel";

async function getTable(title: string, pageHeight: string, page: string) {
  const res = await axios.get(
    `https://localhost:44333/api/${title}s/paged/${pageHeight}/${page}`
  );

  return res.data;
}

export const getRow = async (title: string, id: string) => {
  const res = await axios.get(`https://localhost:44333/api/${title}s/${id}`);

  return res.data;
};

export const deleteRow = async (title: string, id: string) => {
  const res = await axios.delete(`https://localhost:44333/api/${title}s/${id}`);
};

export const addRow = async <
  T extends MovieModel | ActorModel | RatingModel | RoleModel
>(
  title: string,
  inputs: T
) => {
  const res = await axios.post(`https://localhost:44333/api/${title}s`, inputs);

  return res;
};

export const updateRow = async <
  T extends MovieModel | ActorModel | RatingModel | RoleModel
>(
  title: string,
  inputs: T,
  id:string
) => {
  const res = await axios.put(`https://localhost:44333/api/${title}s/${id}`, inputs);

  return res;
};

export default getTable;
