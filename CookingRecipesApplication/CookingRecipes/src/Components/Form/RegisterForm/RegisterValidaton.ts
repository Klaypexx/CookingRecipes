import * as Yup from 'yup';
import {
  NAME_MAX_WORDS,
  NAME_MIN_WORDS,
  PASSWORD_MAX_WORDS,
  PASSWORD_MIN_WORDS,
  USERNAME_MAX_WORDS,
  USERNAME_MIN_WORDS,
} from '../../../Constants/register';

const registerValidation = Yup.object({
  name: Yup.string()
    .required('Имя обязательно')
    .min(NAME_MIN_WORDS, `Минимум ${NAME_MIN_WORDS} символа`)
    .max(NAME_MAX_WORDS, `Максимум ${NAME_MAX_WORDS} символов`),
  username: Yup.string()
    .required('Логин обязателен')
    .min(USERNAME_MIN_WORDS, `Минимум ${USERNAME_MIN_WORDS} символа`)
    .max(USERNAME_MAX_WORDS, `Максимум ${USERNAME_MAX_WORDS} символов`),
  password: Yup.string()
    .required('Пароль обязателен')
    .min(PASSWORD_MIN_WORDS, `Минимум ${PASSWORD_MIN_WORDS} символов`)
    .max(PASSWORD_MAX_WORDS, `Максимум ${PASSWORD_MAX_WORDS} символов`),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref('password')], 'Пароли должны совпадать')
    .required('Подтверждение пароля обязательно'),
});

export default registerValidation;
