import './Course.css'
import {useParams} from 'react-router-dom'
import { Button, Menu } from 'antd';
import { ReadOutlined, PlayCircleOutlined, QuestionCircleOutlined } from '@ant-design/icons';
import { useState, useEffect } from 'react';
import apiClient from "../../apiClient/apiClient"
import Markdown from 'react-markdown'
import CourseContent from '../CourseContent/CourseContent';
import Statistics from '../Statistics/Statistics';

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

                let icon;
                if (item.type === 0){
                  icon = <ReadOutlined style={item.isCommitted ? { color: 'green' } : {}}/>
                }
                else if (item.type === 1){
                  icon = <PlayCircleOutlined style={item.isCommitted ? { color: 'green' } : {}}/>
                }
                else if (item.type === 2){
                  icon = <QuestionCircleOutlined style={item.isCommitted ? { color: 'green' } : {}}/>
                }

                children.push({
                  key: item.id,
                  icon: icon,
                  label: item.name,
                  disabled: courseStructure.deadline == null,
                });
            }

            items.push({
              key: section.id,
              children: children,
              label: section.name,
            });
        }

        items.push({
          key: "stats",
          label: "Статистика",
        });

        return items;
    }

    const fetchCourse = async () => {
      try {
        const courseData = await apiClient.getCourse(id);
        setCourse(courseData);
        let items = getItems(courseData);
        setStructure(items);
      } catch (error) {
        console.error("Error fetching courses:", error);
      }
    };

    async function commitItem(itemId){
        let item = courseStructure.sections
          .flatMap((s) => s.items)
          .find(t => t.id === itemId);

        console.log("try commit", item, itemId)

        if (item && (item.type === 0 || item.type === 1) && !item.isCommitted){
          await apiClient.commitItem(
            courseStructure.courseId,
            courseStructure.learningId,
            itemId
          );

          setCourse((courseStructure) => {
            const newStructure = { ...courseStructure };
            let item = newStructure.sections
              .flatMap((s) => s.items)
              .find((t) => t.id === itemId);

            if (item) {
              item.isCommitted = true;
            }

            return newStructure;
          });

          let items = getItems(courseStructure);
          setStructure(items);
        }
    }

    useEffect(() => {
      fetchCourse();
    }, []);

    function onMenuClick(itemId){
        console.log("click")
        setSelectedItem(itemId)

        commitItem(itemId)
    }

    async function onStartCourseClick() {
      try {
        await apiClient.startLearning(id);
        await fetchCourse();
      } catch (error) {
        console.error("Error starting course:", error);
      }
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
          defaultOpenKeys={[courseStructure.lastCommittedItem]}
        />

        <div className="course-wrapper">
          {selectedItem === courseHome ? (
            <>
              <h1 className="course-header">{courseStructure.courseName}</h1>
              <Markdown>{courseStructure.courseDescription}</Markdown>
              <p>Продолжительность курса {courseStructure.duration} дней</p>
              <p>Курс создан: {getReadableDate(courseStructure.createdAt)}</p>
            </>
          ) : selectedItem.includes("section-description") ? (
            <Markdown>
              {
                courseStructure.sections.find((s) =>
                  selectedItem.includes(s.id)
                ).description
              }
            </Markdown>
          ) : selectedItem === "stats" ? (
            <Statistics courseId={courseStructure.courseId}/>
          ) : (
            <CourseContent
              key={selectedItem}
              itemId={selectedItem}
              courseId={courseStructure.courseId}
              learningId={courseStructure.learningId}
            />
          )}

          {!courseStructure.learningId && (
            <Button
              type="primary"
              size="large"
              className="startCourse-button"
              onClick={onStartCourseClick}
            >
              Записаться
            </Button>
          )}
        </div>
      </div>
    );
}

export default Course