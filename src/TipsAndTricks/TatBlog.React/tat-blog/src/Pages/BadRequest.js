import { Link } from 'react-router-dom';
import { useQuery } from '../Utils/Utils';

const BadRequest = () => {
    let query = useQuery(),
        redirectTo = query.get('redirectTo') ?? '/';
    return (
        <>
            <h1>400 Yêu cầu không hợp lệ</h1>
        </>
    );
}

export default BadRequest;