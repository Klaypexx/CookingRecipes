import * as Yup from 'yup';
import {
  PASSWORD_MAX_WORDS,
  PASSWORD_MIN_WORDS,
  USERNAME_MAX_WORDS,
  USERNAME_MIN_WORDS,
} from '../../../Constants/login';

const loginValidation = Yup.object({
  username: Yup.string()
    .required('Логин обязателен')
    .min(USERNAME_MIN_WORDS, `Минимум ${USERNAME_MIN_WORDS} символа`)
    .max(USERNAME_MAX_WORDS, `Максимум ${USERNAME_MAX_WORDS} символов`),
  password: Yup.string()
    .required('Пароль обязателен')
    .min(PASSWORD_MIN_WORDS, `Минимум ${PASSWORD_MIN_WORDS} символов`)
    .max(PASSWORD_MAX_WORDS, `Максимум ${PASSWORD_MAX_WORDS} символов`),
});

export default loginValidation;
