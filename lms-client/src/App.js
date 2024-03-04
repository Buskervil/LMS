import './App.css';
import React, { useState } from 'react';
import Courses from './components/Courses/Courses';
import { Header } from 'antd/es/layout/layout';
import {Routes, Route, Link} from 'react-router-dom'

import NotFound from './components/NotFound/NotFound'
import Course from './components/Course/Course';

function App() {
  
  return (
      <div className="App">
        <Header/>
        <Routes>
          <Route path="/" element={<Courses />}></Route>
          <Route path="*" element={<NotFound />}></Route>
          <Route path="course/:id" element={<Course />}></Route>
        </Routes>
      </div>
  );
}

export default App;
