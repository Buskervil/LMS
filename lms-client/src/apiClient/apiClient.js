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
        console.log(json)
        return json;
    }
}

export default apiClient
