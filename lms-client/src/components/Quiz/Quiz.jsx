import { Button, Checkbox } from 'antd'
import './Quiz.css'
import { useState } from 'react'

const Quiz = (props) => {

    let [quiz, setQuiz] = useState(props.quiz)

    return (
        <>
            <h1 className='quiz-title'>{quiz.name}</h1>
            {quiz.questions.map((q, i) => (
                <div key={i}>
                    <p key={i}>{q.content}</p>
                    <ul>
                       {q.answers.map((a, ai) => (
                        <li key={ai}>
                            <Checkbox>{a.content}</Checkbox>
                        </li>
                       ))}
                    </ul>
                </div>
            ))}
            <Button type="primary">Завершить тест</Button>
        </>
    )
}

export default Quiz