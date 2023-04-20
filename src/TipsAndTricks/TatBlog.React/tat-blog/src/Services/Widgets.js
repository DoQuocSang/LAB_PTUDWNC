import axios from "axios";

export async function getCategories(pageSize = 10, pageNumber = 1) {
    try {
        const response = await
            axios.get(`https://localhost:7245/api/categories?&PageSize=${pageSize}&PageNumber=${pageNumber}`);
        const data = response.data;
        //console.log('Error',  data.items);
        console.log('Error',  response);

        // if (data.isSuccess)
        //     return data; 
        // else
        //     return null;

        // if (data.isSuccess)
             return data.items; 
        // else
        //     return null;
    } catch (error) {
        console.log('Error', error.message);
        return null;
    }

}