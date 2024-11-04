import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';

// src/types/User.ts
interface User {
  name: string;
  age: number;
  email: string;
}
const REACT_APP_API_URL='https://localhost:7230/Users';

function App() {
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    console.log("Fetching users...");
    // Fetch users from the API
    fetch(`${REACT_APP_API_URL}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Network response was not ok");
        }
        return response.json();
      })
      .then((data: User[]) => setUsers(data))
      .catch((error) => console.error("Error fetching users:", error));
  }, []);

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>Some <code>src/App.tsx</code> and save to reload.</p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Some proje
        </a>
        {/* Render the list of users */}
        <div>
          <h2>Test React fetching for Asp.net Api server:</h2>
          <ul>
            {users.map((user, index) => (
              <li key={index}>
                <strong>Name:</strong> {user.name} <br />
                <strong>Age:</strong> {user.age} <br />
                <strong>Email:</strong> {user.email}
              </li>
            ))}
          </ul>

          <h2>Test Entity framework database working:</h2>
          <ul>
            {/* {users.map((user, index) => (
              <li key={index}>
                <strong>Name:</strong> {user.name} <br />
                <strong>Age:</strong> {user.age} <br />
                <strong>Email:</strong> {user.email}
              </li>
            ))} */}
          </ul>
        </div>
      </header>
    </div>
  );
}

export default App;
