import axios from 'axios';
import { errorToast } from '../Components/Toast/Toast';

export const handleError = (error: any) => {
  if (axios.isAxiosError(error)) {
    var err = error.response;
    if (Array.isArray(err?.data.errors)) {
      for (let val of err?.data.errors) {
        errorToast(val);
      }
    } else if (err?.status == 401) {
      errorToast('Необходимо войти в систему');
    } else if (err) {
      errorToast(err?.data);
    }
  }
};
