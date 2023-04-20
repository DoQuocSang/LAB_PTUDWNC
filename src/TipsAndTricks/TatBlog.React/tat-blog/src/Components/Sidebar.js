import React from 'react';
import SearchForm from './SearchForm';
import CategoriesWidget from './CategoriesWidget';

const Sidebar = () => {
    return (
        <div className='pt-4 ps-2'>
            <SearchForm />

            <CategoriesWidget />

            <h5>
                Tìm kiếm bài viết
            </h5>
            <h5>
                Các chủ đề
            </h5>
            <h5>
                Bài viết nổi bật
            </h5>
            <h5>
                Đăng ký nhận tin mới
            </h5>
            <h5>
                Tag cloud
            </h5>
        </div>
    )
}

export default Sidebar;