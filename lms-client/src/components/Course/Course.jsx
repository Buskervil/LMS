import './Course.css'
import {useParams} from 'react-router-dom'
import { Menu } from 'antd';
import { ReadOutlined, PlayCircleOutlined, QuestionCircleOutlined } from '@ant-design/icons';
import { useState, useEffect } from 'react';
import apiClient from "../../apiClient/apiClient"
import Markdown from 'react-markdown'
import CourseContent from './CourseContent';

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

            if (section.description){
              children.push({
                key: section.id + "section-description",
                label: "Об этом разделе",
              })
            }

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
        const fetchCourse = async () => {
            try {
                const courseData = await apiClient.getCourse(id);
                setCourse(courseData);
                let items = getItems(courseData);
                setStructure(items);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchCourse();

    }, []);

    function onMenuClick(itemId){
        console.log("click")
        setSelectedItem(itemId)
    }

    console.log(selectedItem === courseHome)

    return (
      <div className="course-page">
        <Menu
          mode="inline"
          items={menuStructure}
          className="course-structure-menu"
          defaultSelectedKeys={[courseHome]}
          onClick={(e) => onMenuClick(e.key)}
        />

        <div className="course-wrapper">
          {selectedItem === courseHome ? (
            <>
              <h1 className="course-header">{courseStructure.courseName}</h1>
              <Markdown>{courseStructure.courseDescription}</Markdown>
              <p>Продолжительность курса {courseStructure.duration} дней</p>
              <p>Курс создан: {getReadableDate(courseStructure.createdAt)}</p>
            </>
          ) : selectedItem.includes("section-description")
          ?(<Markdown>{courseStructure.sections.find(s => selectedItem.includes(s.id)).description}</Markdown>)
          :(
            <CourseContent key={selectedItem} itemId={selectedItem} courseId={courseStructure.courseId}/>
          )}
        </div>
      </div>
    );
}

export default Course