import { useState, useEffect, useCallback } from 'react';


const renderAuthors = authors => (
  <table className='table table-striped' aria-labelledby="tabelLabel">
    <thead>
      <tr>
        <th>Author Id</th>
        <th>Name</th>
      </tr>
    </thead>
    <tbody>
      {authors.map((author, i) => <tr key={i}>
        <td>{author.authorId}</td>
        <td>{author.name}</td>
      </tr>
      )}
    </tbody>
  </table>
)

const App = () => {
  const [authors, setAuthors] = useState([])
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    async function populateWeatherData() {
      const response = await fetch('/api/author');
      const data = await response.json();
      setAuthors(data)
      setLoading(false)
      console.log('rerender in useFetch');
    }

    populateWeatherData()
  }, [])

  let contents = loading
    ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
    : renderAuthors(authors);

  return (
    <div>
      <h1 id="tabelLabel">Authors</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {loading
        ? <p>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</p>
        : <h1>hello</h1>}
      {contents}
    </div>
  );
}

export default App
