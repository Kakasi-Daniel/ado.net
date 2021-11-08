let selectedRow = null;

const appendRow = (data) => {
  const row = document.createElement("tr");
  row.setAttribute("data-id", data.id);
  row.innerHTML = `<td>${data.id}</td>
      <td>${data.name}</td>
      <td>${data.releaseDate.slice(0, 10)}</td>
      <td class="actions" ><button data-id=${
        data.id
      } class="actionBtn actionBtnDelete actionBtn--red" >Delete</button><button data-id=${
    data.id
  } class="actionBtn actionBtnUpdate" >Update</button></td>`;

  document.querySelector(".moviesTable").appendChild(row);
};

const displayError = (message) => {
  const error = document.createElement("p");
  error.style.color = "red";
  error.innerText = message;
  document.querySelector(".moviesForm").appendChild(error);
  setTimeout(() => {
    error.remove();
  }, 1500);
};

const getMovies = async () => {
  const res = await fetch("https://localhost:44333/api/Movies");
  const data = await res.json();

  data.forEach((movie) => appendRow(movie));
};

const postMovie = async (movie) => {
  const res = await fetch("https://localhost:44333/api/Movies", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(movie),
  });

  const data = await res.json();
  if (data.name) {
    appendRow(data);
  } else {
    displayError("Server error");
  }
};

const updateMovie = async (movie, id) => {
  const res = await fetch(`https://localhost:44333/api/Movies/${id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(movie),
  });

  console.log(res);

  updateTable();
  normalState();
};

const deleteMovie = async (id) => {
  selectedRow.remove();
  selectedRow = null;
  document.querySelector(".modal").remove();
  await fetch(`https://localhost:44333/api/Movies/${id}`, {
    method: "DELETE",
  });
};

const showModal = (id) => {
  const modal = document.createElement("div");
  modal.classList.add("modal");
  modal.innerHTML = `
    <div class="modalBox">
      <p>Are you sure you want to delete movie with id ${id}?</p>
      <div class="deleteConfirm" >
        <button data-id=${id} class="deleteYes">Yes</button>
        <button class="deleteNo">No</button>
      </div>
    </div>
  `;

  document.querySelector("body").appendChild(modal);
};

const updateTable = () => {
  document.querySelector(".moviesTable").innerHTML = "";
  getMovies();
};

const normalState = () => {
  $("#movieName")
  document.querySelector("#movieName").value = "";
  document.querySelector("#releaseDate").value = "";
  document.querySelector(".moviesSubmit").style.display = "block";
  document.querySelector(".updateButtons").style.display = "none";
  document.querySelector(".state").innerText = "Add movie";
  document.querySelector(".movieUpdateBtn").removeAttribute("data-id");
};

const updateState = async (id) => {
  const res = await fetch(`https://localhost:44333/api/Movies/${id}`);
  const current = await res.json();
  document.querySelector("#movieName").value = current.name;
  document.querySelector("#releaseDate").value = current.releaseDate.slice(
    0,
    10
  );
  document.querySelector(".moviesSubmit").style.display = "none";
  document.querySelector(".updateButtons").style.display = "block";
  document.querySelector(".state").innerText = "Update movie: " + current.name;
  document.querySelector(".movieUpdateBtn").setAttribute("data-id", id);
};

$(document).ready(function () {
  window.onclick = (e) => {
    if (e.target.classList.contains("actionBtnDelete")) {
      selectedRow = e.target.parentElement.parentElement;
      const id = +e.target.getAttribute("data-id");
      showModal(id);
    }
    if (e.target.classList.contains("deleteYes") && selectedRow != null) {
      const id = +e.target.getAttribute("data-id");
      deleteMovie(id);
    }
    if (e.target.classList.contains("deleteNo")) {
      e.target.parentElement.parentElement.parentElement.remove();
    }
    if (e.target.classList.contains("modal")) {
      e.target.remove();
    }
    if (e.target.classList.contains("actionBtnUpdate")) {
      updateState(+e.target.getAttribute("data-id"));
    }
    if (e.target.classList.contains("addState")) {
      normalState();
    }
    if (e.target.classList.contains("movieUpdateBtn")) {
      e.preventDefault();
      const id = +e.target.getAttribute("data-id");
      const movie = {};
      movie.name = document.querySelector("#movieName").value;
      movie.releaseDate = document.querySelector("#releaseDate").value;

      if (movie.name != "" && movie.releaseDate != "") {
        document.querySelector("#movieName").value = "";
        document.querySelector("#releaseDate").value = "";
        updateMovie(movie, id);
      } else {
        displayError("All fields are mandatory!");
      }
    }
  };

  document.querySelector(".moviesSubmit").addEventListener("click", (e) => {
    e.preventDefault();
    const movie = {};
    movie.name = document.querySelector("#movieName").value;
    movie.releaseDate = document.querySelector("#releaseDate").value;

    if (movie.name != "" && movie.releaseDate != "") {
      document.querySelector("#movieName").value = "";
      document.querySelector("#releaseDate").value = "";
      postMovie(movie);
    } else {
      displayError("All fields are mandatory!");
    }
  });

  getMovies();
});
