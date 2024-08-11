export default interface ButtonBlockProps {
  primaryType?: 'button' | 'reset' | 'submit' | undefined;
  secondaryType?: 'button' | 'reset' | 'submit' | undefined;
  primaryButtonText?: string;
  secondaryButtonText?: string;
  onClickPrimary?: () => void;
  onClickSecondary?: () => void;
}
