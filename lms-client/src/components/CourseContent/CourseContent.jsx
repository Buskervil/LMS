import { useState, useEffect } from "react"
import apiClient from "../../apiClient/apiClient"
import Markdown from 'react-markdown'
import './CourseContent.css'
import Quiz from "../Quiz/Quiz"

const CourseContent = (props) => {

    let [item, setItem] = useState({type: 0, content: "asdfqwer"})

    useEffect(() => {
        const fetchItem = async () => {
            try {
                const itemData = await apiClient.getCourseItem(props.courseId, props.itemId);
                setItem(itemData);
            } catch (error) {
                console.error('Error fetching courses:', error);
            }
        };

        fetchItem();

    }, []);

    return (
      <>
        {item.type === 0 ? (
          <div>
            <Markdown>{item.content}</Markdown>
          </div>
        ) : item.type === 1 ? (
          <>
            <h1 className="video-header">{item.name}</h1>
            <iframe
              width="100%"
              height="800px"
              sandbox="allow-same-origin allow-forms allow-popups allow-scripts allow-presentation"
              src={item.source}
              title="Параметры в ссылках в React Router 6"
              frameborder="0"
              allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share"
              allowfullscreen
            ></iframe>
          </>
        ) : (
          <Quiz quiz={item}/>
        )}
      </>
    );
}

export default CourseContent