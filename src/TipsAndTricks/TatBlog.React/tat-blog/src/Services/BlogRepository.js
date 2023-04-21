import axios from 'axios';
import { get_api } from './Methods';

export function getPosts(
    pageSize = 10, 
    pageNumber = 1,
    keyword = '',
    publishedOnly = true,
    NotPublished = false,
    sortColumn = '',
    sortOrder = '') {
    return get_api(`https://localhost:7245/api/posts?PublishedOnly=${publishedOnly}&NotPublished=${NotPublished}&keyword=${keyword}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`)
}

export function getFilter() {
    return get_api('https://localhost:7245/api/posts/get-filter');
}
