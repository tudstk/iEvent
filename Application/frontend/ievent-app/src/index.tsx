import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import './output.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import AuthInput from './auth/AuthInput';
import Panel from './user/Panel';
import { dummyUser, dummyEvents } from './models/dummy';
import EventPage from './event/EventPage';
import Home from './home/Home';

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
  <React.StrictMode>
    {/* <App /> */}
    {/* <AuthInput /> */}
    {/* <Panel user={dummyUser} /> */}
    {/* <EventPage
      event={dummyEvents[0]}
      user={dummyUser}
    /> */}
    <Home />
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
