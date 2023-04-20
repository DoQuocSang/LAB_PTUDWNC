import React, { useEffect, useState } from "react";
import { useLocation } from "react-router-dom";
import PostItem from "../Components/PostItem";
import Pager from "../Components/Pager";
import { getPosts } from "../Services/BlogRepository";

const Index = () => {
    const [postsList, setPostsList] = useState([]);
    const [metadata, setMetadata] = useState([]);

    function useQuery() {
        const { search } = useLocation();
        return React.useMemo(() => new URLSearchParams(search), [search]);
    }

    let query = useQuery(),
        k = query.get('k') ?? '',
        p = query.get('PageNumber') ?? 1,
        ps = query.get('PageSize') ?? 10;

    p = parseInt(p, 10);
    ps = parseInt(ps, 10);

    useEffect(() => {
        document.title = 'Trang chủ';

        getPosts(ps, p).then(data => {
            if (data){
                setPostsList(data.result.items);
                setMetadata(data.result.metadata);
            }
            else
                setPostsList([]);
            //console.log(data.result.items)
        })
    }, [k, p, ps]);

    if (postsList.length > 0)
        return (
            <div className='p-4'>
                {postsList.map((item, index) => {
                    return (
                        <PostItem postItem={item} key={index} />
                    );
                })};
                <Pager postquery={{ 'keyword': k }} metadata={metadata}/>
            </div>
        );
    else return (
        <></>
    )
}

export default Index;