import axios from "axios";
import { UserProfileToken } from "../Models/User";

const api = "https://localhost:53316/users/";

export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "login", {
      username: username, 
      password: password,
    });
    console.log(data.data);
  } catch (error) {
    console.log(error);
  }
};

export const registerAPI = async (
  name: string,
  username: string,
  password: string
) => {
  try {
    const data = await axios.post<UserProfileToken>(api + "register", {
      name: name,
      username: username,
      password: password,
    });
    console.log(data);
  } catch (error) {
    console.log(error);
  }
};
