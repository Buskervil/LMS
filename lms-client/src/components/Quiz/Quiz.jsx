import { Button, Checkbox, Progress } from 'antd'
import './Quiz.css'
import { useState } from 'react'
import apiClient from "../../apiClient/apiClient"


const Quiz = (props) => {

    let [quiz, setQuiz] = useState(props.quiz)
    let [answers, setAnswers] = useState([])
    let [result, setResult] = useState(-1)

    function onChecked(added, question, answer){
        console.log("cheked", added, question, answer)
    
        setAnswers(answers => {
            const newanswers = [...answers]

            let solvedQuestion = newanswers.find(q => q.id == question.id)
            if (!solvedQuestion)
            {
                solvedQuestion = {
                    id: question.id,
                    SolvedAnswers: []
                }

                newanswers.push(solvedQuestion)
            }

            solvedQuestion.SolvedAnswers = solvedQuestion.SolvedAnswers.filter(a => a.id !== answer.id)
            //if (added){
                solvedQuestion.SolvedAnswers.push({
                    id: answer.id,
                    questionId: question.id,
                    isSelected: added
                })
            //}

            console.log("Вопросы", newanswers)
            return newanswers
        })
    }

    async function submit(){
        console.log(props)
        let result = await apiClient.commitQuiz(props.courseId, props.learningId, props.quizId, answers)

        setResult(result.value)

        console.log(result)
    }

    return (
      <>
        <h1 className="quiz-title">{quiz.name}</h1>
        {quiz.questions.map((q, i) => (
          <div key={i}>
            <p key={i}>{q.content}</p>
            <ul>
              {q.answers.map((a, ai) => (
                <li key={ai}>
                  <Checkbox onChange={(e) => onChecked(e.target.checked, q, a)}>
                    {a.content}
                  </Checkbox>
                </li>
              ))}
            </ul>
          </div>
        ))}
        <Button type="primary" onClick={submit} className="submit-btn">
          Завершить тест
        </Button>

        {result >= 0 && (
          <div>
            <Progress
              type="circle"
              percent={result}
              status={result < 50 ? "exception" : ""}
            />

            {result == 100 && <p>Отлично, все верно!</p>}
            {result < 50 && <p>Не получилось, попробуйте еще раз</p>}
          </div>
        )}
      </>
    );
}

export default Quiz