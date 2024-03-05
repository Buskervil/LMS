import { Progress } from "antd"
import './Statistics.css'
import apiClient from "../../apiClient/apiClient"
import { useState, useEffect } from "react"

const Statistics = (props) => {

    let [statistics, setStatistics] = useState({})

    async function fetchStatistics() {
        let result = await apiClient.getStatistics(props.courseId);
        console.log(result)
        setStatistics({
            totalProgress: result.value.totalProgress,
            totalScore: result.value.totalScore
        })
    }

    useEffect(() => {
        fetchStatistics();
      }, []);

    return (
        <>
        <h1 className="statistics-header">Статистика вашего обучения</h1>
              <div className="statistics">
        <div>
          <Progress type="circle" percent={statistics.totalProgress} />{" "}
          <p>Прогресс</p>
        </div>
        <div>
          <Progress type="circle" percent={statistics.totalScore} />{" "}
          <p>Средний балл</p>
        </div>
      </div>
        </>

    );
}

export default Statistics