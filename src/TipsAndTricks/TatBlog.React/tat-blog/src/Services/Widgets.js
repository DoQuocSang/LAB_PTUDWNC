import axios from "axios";
import { get_api } from './Methods';

export async function getCategories(pageSize = 10, pageNumber = 1) {
    return get_api(`https://localhost:7245/api/categories?&PageSize=${pageSize}&PageNumber=${pageNumber}`);
    
}

