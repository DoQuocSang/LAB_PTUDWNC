import React, { useEffect, useState } from "react";
import PostItem from "../Components/PostItem";
import { getPosts } from "../Services/BlogRepository";

const Index = () => {
    const [postsList, setPostsList] = useState([]);

    useEffect(() => {
        document.title = 'Trang chủ';

        getPosts().then(data => {
            if (data)
                setPostsList(data.result.items);
            else
                setPostsList([]);
        })
    }, []);

    if (postsList.length > 0)
        return (
            <div className='p-4'>
                {postsList.map((item, index) => {
                    return (
                        <PostItem postItem={item} key={index} />
                    );
                })};
            </div>
        );
    else return (
        <></>
    )
}

export default Index;