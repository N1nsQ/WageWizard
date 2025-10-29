import type { ReactElement } from "react";

export interface IconLinkCellProps {
  to: string;
  icon: ReactElement;
  className?: string;
  label?: string;
  testId?: string;
}
