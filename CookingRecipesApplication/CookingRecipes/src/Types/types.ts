import { CSSProperties } from 'react';

interface ButtonProps {
  primary?: boolean;
  className?: string;
  type?: 'button' | 'reset' | 'submit' | undefined;
  navigation?: string;
  newStyle?: CSSProperties;
  buttonText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}

interface ButtonBlockProps {
  primaryType?: 'button' | 'reset' | 'submit' | undefined;
  secondaryType?: 'button' | 'reset' | 'submit' | undefined;
  primaryButtonText?: string;
  secondaryButtonText?: string;
  onClickPrimary?: () => void;
  onClickSecondary?: () => void;
}

interface CardProps {
  margin?: boolean;
  className?: string;
  children?: React.ReactNode;
}

interface FormProps {
  primary?: boolean;
  initialValues: any;
  validationSchema?: any;
  onSubmit: (values: any) => void;
  errorText?: string;
  children?: React.ReactNode;
}

interface FormInputProps {
  margin?: boolean;
  select?: boolean;
  type?: string;
  as?: string;
  className?: string;
  name: string;
  placeholder?: string;
  maxLength?: number;
  styles?: string;
  children?: React.ReactNode;
}

interface TagsInputProps {
  name: string;
}

interface LinkProps {
  primary?: boolean;
  navigation?: string;
  newStyle?: CSSProperties;
  linkText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}

interface LinkBlockProps {
  linkPrimaryText: string;
  linkSecondaryText: string;
  onClickPrimary?: () => void;
  onClickSecondary?: () => void;
}

interface ModalProps {
  primary?: boolean;
  headerClassName?: string | string[];
  haederText?: string;
  hasAccountText?: string;
  children?: React.ReactNode;
}

interface LoginValues {
  username: string;
  password: string;
}

interface RegisterValues {
  name: string;
  username: string;
  password: string;
}

interface SubheaderProps {
  backward?: boolean;
  type?: 'button' | 'reset' | 'submit' | undefined;
  headerText: string;
  btn?: boolean;
  buttonText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}

interface RecipeFormValues {
  recipeName: string;
  description: string;
  tags: string[];
  cookingTime: string;
  portion: string;
  step: string;
  ingredient: {
    header: string;
    products: string;
  };
}

interface ModalStore {
  isAuth: boolean;
  isLogin: boolean;
  isRegister: boolean;
  isLogout: boolean;
  setAuth: (state: boolean) => void;
  setLogin: (state: boolean) => void;
  setRegister: (state: boolean) => void;
  setLogout: (state: boolean) => void;
  unsetAll: () => void;
}

export type {
  ButtonProps,
  ButtonBlockProps,
  CardProps,
  FormProps,
  FormInputProps,
  TagsInputProps,
  LinkProps,
  LinkBlockProps,
  ModalProps,
  LoginValues,
  RegisterValues,
  SubheaderProps,
  RecipeFormValues,
  ModalStore,
};
