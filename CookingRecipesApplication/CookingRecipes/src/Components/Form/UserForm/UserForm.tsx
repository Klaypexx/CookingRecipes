import UserFormValues from '../../../Types/UserFormValues';
import BaseField from '../../Field/BaseField/BaseField';
import BaseForm from '../BaseForm/BaseForm';
import updatePencil from '../../../resources/icons/updatePencil.svg';
import styles from './UserForm.module.css';
import UserFormProps from '../../../Types/UserFormProps';

const UserForm: React.FC<UserFormProps> = ({ values }) => {
  const initialValues: UserFormValues = {
    ...values,
    description: values.description ? values.description : '',
    password: '',
  };

  return (
    <div className={styles.userProfileContainer}>
      <div>
        <BaseForm
          initialValues={initialValues}
          onSubmit={function (values: any): void {
            console.log(values);
          }}
        >
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
