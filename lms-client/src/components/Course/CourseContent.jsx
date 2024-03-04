import { useState, useEffect } from "react"
import apiClient from "../../apiClient/apiClient"
import Markdown from 'react-markdown'


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
        ) : (
          <div />
        )}
      </>
    );
}

export default CourseContent