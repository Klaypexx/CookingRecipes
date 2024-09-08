export default interface BaseFieldProps {
  margin?: boolean;
  select?: boolean;
  type?: string;
  as?: string;
  className?: string;
  name: string;
  labelText?: string;
  placeholder?: string;
  maxLength?: number;
  autocomplete?: boolean;
  styles?: string;
  children?: React.ReactNode;
}
