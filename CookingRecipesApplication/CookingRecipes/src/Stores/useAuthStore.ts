import { create } from "zustand";
import TokenService from "../Services/TokenService";

export const useAuthStore = create(() => ({
    token: TokenService.getAccessToken()
}))