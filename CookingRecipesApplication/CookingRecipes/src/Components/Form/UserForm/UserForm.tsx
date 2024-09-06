import updatePencil from '../../../resources/icons/updatePencil.svg';
import AuthService from '../../../Services/AuthService';
import UserService from '../../../Services/UserService';
import useUserStore from '../../../Stores/useUserStore';
import UserFormProps from '../../../Types/UserFormProps';
import UserFormValues from '../../../Types/UserFormValues';
import BaseField from '../../Field/BaseField/BaseField';
import { successToast } from '../../Toast/Toast';
import BaseForm from '../BaseForm/BaseForm';
import styles from './UserForm.module.css';
import userValidation from './UserValidation';

const UserForm: React.FC<UserFormProps> = ({ values }) => {
  const { setUserName, setNameOfUser } = useUserStore();

  const initialValues: UserFormValues = {
    ...values,
    password: '',
  };

  const handleSubmit = async (values: UserFormValues) => {
    let formData = new FormData();
    formData.append('Name', values.name);
    formData.append('UserName', values.userName);

    if (values.description) {
      formData.append('Description', values.description);
    }

    if (values.password) {
      formData.append('Password', values.password);
    }

    await UserService.updateUser(formData).then((res) => {
      if (res) {
        successToast('Пользователь успешно обновлен');
      }
    });

    await AuthService.refresh().then((res) => {
      if (res) {
        setUserName(values.userName);
        setNameOfUser(values.name);
      }
    });
  };

  return (
    <div className={styles.userProfileContainer}>
      <div>
        <BaseForm initialValues={initialValues} validationSchema={userValidation} onSubmit={handleSubmit}>
          <div className={styles.updateBox}>
            <button type="submit">
              <img src={updatePencil} alt="updatePencil" className={styles.updatePencil} />
            </button>
          </div>
          <div className={styles.userInformation}>
            <div>
              <BaseField className={styles.field} name="name" labelText="Имя" margin />
              <BaseField className={styles.field} name="userName" labelText="Логин" margin />
              <BaseField className={styles.field} name="password" type="password" placeholder="Пароль" />
            </div>
            <div>
              <BaseField
                name="description"
                as="textarea"
                labelText="Напишите немного о себе"
                className={styles.description}
              />
            </div>
          </div>
        </BaseForm>
      </div>
    </div>
  );
};

export default UserForm;
