import {Link} from 'react-router-dom'
import './Header.css'

const Header = () => {

    return (
        <header>
            <Link className='company-name' to="/">LMS</Link>
        </header>
    )
}

export default Header