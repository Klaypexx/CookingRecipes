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

interface AddRecipeButtonProps {
  primary?: boolean;
  buttonText?: string;
  className?: string;
  onClick?: () => void;
}

interface CardProps {
  className?: string;
  props?: {
    idRecipe: number;
    name: string;
    description: string;
    avatarPath?: string;
    authorName: string;
    tags?: Array<{
      name: string;
    }>;
    cookingTime: number;
    portion: number;
  };
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

interface BaseFieldProps {
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

interface TagsFieldProps {
  name: string;
}

interface IngredientFieldProps {
  name: string;
}

interface StepFieldProps {
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
  headerText: string;
  children?: React.ReactNode;
}

interface RecipeFormValues {
  name: string;
  description: string;
  avatar: File | null;
  tags: string[];
  cookingTime: number;
  portion: number;
  steps: Array<{
    description: string;
  }>;
  ingredients: Array<{
    name: string;
    product: string;
  }>;
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

interface TagsBlockListProps {
  text?: boolean;
  className?: string;
}

interface SearchBlockProps {
  text?: boolean;
  className?: string;
  onSubmit: (values: any) => void;
}

interface SearchBlockValues {
  name: string;
}

interface RecipeListValues {
  idRecipe: number;
  name: string;
  description: string;
  avatarPath?: string;
  authorName: string;
  tags?: Array<{
    name: string;
  }>;
  cookingTime: number;
  portion: number;
}

export type {
  ButtonProps,
  ButtonBlockProps,
  AddRecipeButtonProps,
  CardProps,
  FormProps,
  BaseFieldProps,
  TagsFieldProps,
  IngredientFieldProps,
  StepFieldProps,
  LinkProps,
  LinkBlockProps,
  ModalProps,
  LoginValues,
  RegisterValues,
  SubheaderProps,
  RecipeFormValues,
  ModalStore,
  TagsBlockListProps,
  SearchBlockProps,
  SearchBlockValues,
  RecipeListValues,
};
