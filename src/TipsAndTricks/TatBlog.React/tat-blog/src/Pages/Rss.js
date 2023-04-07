import React, {useEffect} from "react";

const Rss = () => {
    useEffect(() => {
        document.title = 'Liên kết';
    }, []);

    return(
        <h1>
            Đây là trang liên kết
        </h1>
    );
}

export default Rss;