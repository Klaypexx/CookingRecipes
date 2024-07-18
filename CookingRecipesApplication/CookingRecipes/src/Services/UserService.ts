import api from "../util/api";

const endpoints = {
    username: '/users/username',
};

const username = async () => {
  const response = await api
        .get(endpoints.username);
    return response.data;
};

const UserService = {
    username
};

export default UserService;