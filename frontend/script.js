let currentPage = 1;

const appendRow = (data) => {
  const row = $("<tr></tr>").appendTo(".moviesTable");
  row.attr("data-id", data.id);
  row.html(`<td>${data.id}</td>
  <td>${data.name}</td>
  <td>${data.releaseDate.slice(0, 10)}</td>
  <td class="actions" ><button data-id=${
    data.id
  } class="actionBtn actionBtnDelete actionBtn--red" >Delete</button><button data-id=${
    data.id
  } class="actionBtn actionBtnUpdate" >Update</button></td>`);
};

const drawButtons = (buttons, current) => {
  $(".buttons").html("");
  const buttonsContainer = $("<ul></ul>").appendTo(".buttons");
  buttonsContainer.addClass("pageButtonsList");

  for (let i = 1; i <= buttons; i++) {
    const buttonItem = $("<li></li>").appendTo(buttonsContainer);
    const button = $("<button></button>").appendTo(buttonItem);
    button.text(i);
    button.addClass("pageButton");
    if (current === i) {
      button.addClass("current");
    }
    button.attr("data-page", i);
  }
};

const displayError = (message) => {
  const error = $("<p></p>").appendTo(".moviesForm");
  error.css("color", "red");
  error.text(message);
  setTimeout(() => {
    error.remove();
  }, 1500);
};

const getMovies = async (page, pageSize = 10) => {
  const tableHeight = $(".moviesTable")?.height();
  const tableWidth = $(".moviesTable")?.width();
  $(".moviesTable").css("height", tableHeight);
  $(".moviesTable").css("width", tableWidth);
  $(".moviesTable").html(`<div class="spinner" ></div>`);

  const res = await fetch(
    `https://localhost:44333/api/Movies/paged/${pageSize}/${page}`
  );
  const data = await res.json();
  currentPage = data.page;
  $(".moviesTable").html(`
  <tr>
    <th>Id</th>
    <th>Name</th>
    <th>Release Date</th>
    <th>Actions</th>
  </tr>`);

  data.data.forEach((movie) => appendRow(movie));
  $(".moviesTable").css("height", "auto");
  $(".moviesTable").css("width", "auto");
  drawButtons(data.pages, currentPage);
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
  await fetch(`https://localhost:44333/api/Movies/${id}`, {
    method: "DELETE",
  });
  updateTable();
  $(".modal").remove();
};

const showModal = (id) => {
  const modal = $("<div></div>").appendTo("body");
  modal.addClass("modal");
  modal.html(`
  <div class="modalBox">
    <p>Are you sure you want to delete movie with id ${id}?</p>
    <div class="deleteConfirm" >
      <button data-id=${id} class="deleteYes">Yes</button>
      <button class="deleteNo">No</button>
    </div>
  </div>`);
};

const updateTable = () => {
  $(".moviesTable").html("");
  getMovies(currentPage);
};

const normalState = () => {
  $("#movieName").val("");
  $("#releaseDate").val("");
  $(".moviesSubmit").css("display", "block");
  $(".updateButtons").css("display", "none");
  $(".state").text("Add movie");
  $(".movieUpdateBtn").removeAttr("data-id");
};

const updateState = async (id) => {
  const res = await fetch(`https://localhost:44333/api/Movies/${id}`);
  const current = await res.json();
  $("#movieName").val(current.name);
  $("#releaseDate").val(current.releaseDate.slice(0, 10));
  $(".moviesSubmit").css("display", "none");
  $(".updateButtons").css("display", "block");
  $(".state").text("Update movie: " + current.name);
  $(".movieUpdateBtn").attr("data-id", id);
};

$(document).ready(() => {
  window.onclick = (e) => {
    if (e.target.classList.contains("actionBtnDelete")) {
      selectedRow = e.target.parentElement.parentElement;
      const id = +e.target.getAttribute("data-id");
      showModal(id);
    }
    if (e.target.classList.contains("deleteYes")) {
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

    if (e.target.classList.contains("pageButton")) {
      const page = +e.target.getAttribute("data-page");
      getMovies(page);
    }

    if (e.target.classList.contains("movieUpdateBtn")) {
      e.preventDefault();
      const id = +e.target.getAttribute("data-id");
      const movie = {};
      movie.name = $("#movieName").val();
      movie.releaseDate = $("#releaseDate").val();

      if (movie.name != "" && movie.releaseDate != "") {
        $("#movieName").val("");
        $("#releaseDate").val("");
        updateMovie(movie, id);
      } else {
        displayError("All fields are mandatory!");
      }
    }
  };

  $(".moviesSubmit").on("click", (e) => {
    e.preventDefault();
    const movie = {};
    movie.name = $("#movieName").val();
    movie.releaseDate = $("#releaseDate").val();
    console.log(movie.name);
    console.log(movie.releaseDate);

    if (movie.name != "" && movie.releaseDate != "") {
      $("#movieName").val("");
      $("#releaseDate").val("");
      postMovie(movie);
    } else {
      displayError("All fields are mandatory!");
    }
  });

  getMovies(currentPage);
});
