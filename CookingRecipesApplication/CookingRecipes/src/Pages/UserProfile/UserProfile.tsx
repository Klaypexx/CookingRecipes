import { useEffect, useState } from 'react';
import UserForm from '../../Components/Form/UserForm/UserForm';
import Subheader from '../../Components/Subheader/Subheader';
import styles from './UserProfile.module.css';
import UserService from '../../Services/UserService';
import UserValues from '../../Types/UserValues';
import Spinner from '../../Components/Spinner/Spinner';

const UserProfile = () => {
  const [loading, setLoading] = useState(true);
  const [user, setUser] = useState<UserValues>();

  useEffect(() => {
    const fetchUser = async () => {
      await UserService.getUser().then((res) => {
        if (res) {
          console.log(res.response.data);
          setUser(res.response.data);
        }
      });
      setLoading(false);
    };
    fetchUser();
  }, []);

  if (loading) {
    return <Spinner />;
  }

  return (
    <div className={styles.userProfile}>
      <section>
        <Subheader backward text="Мой профиль" />
      </section>
      <section>
        <UserForm values={user!} />
      </section>
    </div>
  );
};

export default UserProfile;
