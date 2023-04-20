import axios from 'axios';

export async function getPosts(pageSize = 10, pageNumber = 1, publishedOnly = true, NotPublished = false, keyword = '', 
    sortColumn = '', sortOrder = '') {
    try {
        const response = await
            axios.get(`https://localhost:7245/api/posts?PublishedOnly=${publishedOnly}&NotPublished=${NotPublished}&keyword=${keyword}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`)
            //axios.get(`https://localhost:7245/api/posts/featured/{limit}?numPosts=10`)
            //axios.get(`https://reqres.in/api/users/2`)
            // .then(res => {
            //      console.log("respone value", res.data.result.items);
            // }) 

        const data = response.data; 
        console.log("respone value", response);

        if (data.isSuccess)
            return data;
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}