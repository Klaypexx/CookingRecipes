import { create } from "zustand";
import TokenService from "../Services/TokenService";

interface AuthStore {
    token: string | Error | null;
    setToken: (newToken: string) => void;
}

export const useAuthStore = create<AuthStore>()((set) => ({
    token: TokenService.getAccessToken(),
    setToken: (newToken: string) => set(() => ({token: newToken})),
}))