import { AxiosResponse } from 'axios';
import { handleError } from '../Helpers/ErrorHandler';
import api from '../util/api';

const endpoints = {
  getRandomTags: '/tags/random',
};

const getRandomTags = async () => {
  try {
    const response: AxiosResponse<string[], any> = await api.get(`${endpoints.getRandomTags}`);
    return { response };
  } catch (error) {
    handleError(error);
  }
};

const TagService = {
  getRandomTags,
};

export default TagService;
