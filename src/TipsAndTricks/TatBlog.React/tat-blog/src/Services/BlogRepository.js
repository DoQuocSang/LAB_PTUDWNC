import axios from 'axios';

export async function getPosts(keyword = '', pageSize = 10, pageNumber = 1,
    sortColumn = '', sortOrder = '') {
    try {
        const response = await
            axios.get(`https://localhost:7245/api/posts?keyword=${keyword}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`)
            //axios.get(`https://localhost:7245/api/posts/featured/{limit}?numPosts=10`)
            //axios.get(`https://reqres.in/api/users/2`)
            // .then(res => {
            //      console.log("respone value", res.data.result.items);
            // }) 
        const data = response.data;
        if (data.isSuccess)
            return data;
        else
            return null;
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }
}