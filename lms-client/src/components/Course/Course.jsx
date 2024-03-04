import './Course.css'
import {useParams} from 'react-router-dom'
import { Menu } from 'antd';
import { ReadOutlined, PlayCircleOutlined, QuestionCircleOutlined } from '@ant-design/icons';
import { useState, useEffect } from 'react';
import apiClient from "../../apiClient/apiClient"


const Course = () => {

    const courseHome = "main"
    let {id} = useParams();
    let [menuStructure, setStructure] = useState([])
    let [selectedItem, setSelectedItem] = useState(courseHome)
    let [courseStructure, setCourse] = useState({})

    function getReadableDate(date){

        let parsedDate = new Date(date);
        return  `${String(parsedDate.getMonth() + 1).padStart(2, '0')}.${parsedDate.getFullYear()}`
    }

    function getItems(courseStructure) {

        var items = []

        items.push({
            key: courseHome,
            label: "О курсе",
          });

        for (let section of courseStructure.sections){
            let children = []

            for (let item of section.items){

                let icon = item.type === 0
                  ? (<ReadOutlined />)
                  : item.Type === 1 ? (<PlayCircleOutlined />) : (<QuestionCircleOutlined />);
                
                children.push({
                  key: item.id,
                  icon: icon,
                  label: item.name,
                });
            }

            items.push({
              key: section.id,
              children: children,
              label: section.name,
            });
        }

        return items;
    }

    useEffect(() => {
        const fetchCourses = async () => {
            try {
                const coursesData = await apiClient.getCourse(id);
                setCourse(coursesData);
                let items = getItems(coursesData);
                setStructure(items);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourses();

    }, []);

    function onMenuClick(itemId){
        console.log("click")
        setSelectedItem(itemId)
    }

    console.log(selectedItem === courseHome)

    return (
      <div className='course-page'>

        <Menu
          mode="inline"
          items={menuStructure}
          className="course-structure-menu"
          defaultSelectedKeys={[courseHome]}
          onClick={(e) => onMenuClick(e.key)}
        />

        {selectedItem === courseHome
            ?
                <div className='course-wrapper'>
                    <h1>{courseStructure.courseName}</h1>
                    <p>{courseStructure.courseDescription}</p>
                    <p>Продолжительность курса {courseStructure.duration} дней</p>
                    <p>Курс создан: {getReadableDate(courseStructure.createdAt)}</p>
                </div>
            : 
                <div/>
        }
      </div>
    );
}

export default Course