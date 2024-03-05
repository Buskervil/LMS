const baseUrl = "http://localhost:5186"

const apiClient = {

    getCourses: async () => {
        let response = await fetch(
            `${baseUrl}/api/course`,
            {
              method: 'get'
            }
        ) 
        
        let json = await response.json();
        return json;
    },

    getCourse: async (courseId) => {
        let response = await fetch(
            `${baseUrl}/api/course/${courseId}`,
            {
              method: 'get'
            }
        ) 
        
        let json = await response.json();
        console.log(json)
        return json;
    },

    getCourseItem: async (courseId, itemId) => {
        let response = await fetch(
            `${baseUrl}/api/course/${courseId}/item/${itemId}`,
            {
              method: 'get'
            }
        ) 
        
        let json = await response.json();
        console.log(json)
        return json;
    },

    startLearning: async (courseId) => {
        const startLearningData = {
            CourseId: courseId
        };
    
        console.log(startLearningData)
        let response = await fetch(
            `${baseUrl}/api/learning/StartLearning`,
            {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(startLearningData)
            }
        );
    
        let json = await response.json();
        console.log(json);
        return json;
    },

    commitItem: async (courseId, learningId, itemId) => {
        const startLearningData = {
            CourseId: courseId,
            ItemId: itemId,
            LearningId: learningId
        };
    
        console.log(startLearningData)
        let response = await fetch(
            `${baseUrl}/api/learning/CommitItem`,
            {
                method: 'post',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(startLearningData)
            }
        );
    
        let json = await response.json();
        console.log(json);
        return json;
    }
}

export default apiClient
