import './CoursesPage.css'
import { Card, Flex } from "antd";
import {Link} from 'react-router-dom'

const CoursesPage = (props) => {

    return (
      <div className={`courses-page ${props.className}`}>
        <h2 className="coursesPage-header">{props.title}</h2>
        <div className="courses-list">
          {props.courses.map((c) => (
            <Link to={`/course/${c.id}`} className="link">
              <Card title={c.name} className="courseCard">
                <div className='card-content'>
                  <p><b>Описание:</b> {c.description.substring(0, 180)}...</p>
                  <p>
                    <b>Продолжительность:</b> {c.duration} дней
                  </p>
                </div>
              </Card>
            </Link>
          ))}
        </div>
      </div>
    );
}

export default CoursesPage;