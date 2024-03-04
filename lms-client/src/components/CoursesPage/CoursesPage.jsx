import './CoursesPage.css'
import { Card, Flex } from "antd";
import {Link} from 'react-router-dom'

const CoursesPage = (props) => {

    return (
      <div className={`courses-page ${props.className}`}>
        <h2 className="coursesPage-header">{props.title}</h2>
        <div className="courses-list">
          {props.courses.map((c) => (
            <Link to={`/course/${c.id}`} className='link'>
              <Card title={c.name} className="courseCard">
                <p>{c.description}</p>
                <p>Продолжительность {c.duration} дней</p>
                <p>{c.description}</p>
              </Card>
            </Link>
          ))}
        </div>
      </div>
    );
}

export default CoursesPage;