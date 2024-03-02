import logo from './logo.svg';
import './App.css';
import React, { useState } from 'react';

function App() {
  
  let [forecast, setForecast] = useState([])
  
  let loadForecast = async () => {
    let response = await fetch(
        'api/weatherforecast',
        {
          method: 'get'
        }
    ) 
    
    let json = await response.json();
    console.log(json)
    setForecast(json)
  }
  
  return (
    <div className="App">
        {forecast.map((f, i) =>
        <div>
          <p>{f.date}</p>
          <p>{f.summary}</p>
          <p>{f.temperatureC}</p>
        </div>)}
      <button onClick={loadForecast}>Загрузить прогноз</button>
    </div>
  );
}

export default App;
