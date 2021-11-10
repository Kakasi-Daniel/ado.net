import Table from "./Components/Table"

const App = () => {
    return (
        <>
          <Table title="Movie" url="https://localhost:44333/api/Movies"/>
          <Table title="Actor"  url="https://localhost:44333/api/Actors"/>
          <Table title="Role"  url="https://localhost:44333/api/Roles"/>
          <Table title="Rating" url="https://localhost:44333/api/Ratings"/>
        </>
    )
}

export default App
