import { CSSProperties } from 'react';

export default interface LinkProps {
  base?: boolean;
  primary?: boolean;
  navigation?: string;
  newStyle?: CSSProperties;
  linkText?: string;
  onClick?: () => void;
  children?: React.ReactNode;
}
