import api from "../util/api";

const endpoints = {
    username: '/users/username',
};

const username = async () => {
    const response = await api
        .get(endpoints.username);
    if (response.data && response.data !== undefined) {
        return response.data;
    }
    return response.statusText;
};

const UserService = {
    username
};

export default UserService;