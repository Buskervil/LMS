import { Card, Flex } from "antd";
import { useState, useEffect } from "react";
import './Courses.css';
import CoursesPage from "../CoursesPage/CoursesPage";
import apiClient from "../../apiClient/apiClient"

const Courses = () => {

    const [courses, setCourses] = useState([]);

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const coursesData = await apiClient.getCourses();
                setCourses(coursesData);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();
    }, []);

    return (
      <div className="wrapper">
        <h1 className="courses-header">Образование</h1>
        <div className="courses">
          <CoursesPage
            className="half-width"
            courses={courses}
            title="Каталог курсов"
          />
          <CoursesPage
            className="half-width"
            courses={courses.filter(c => c.started)}
            title="Мои курсы"
          />
        </div>
      </div>
    );
}

export default Courses