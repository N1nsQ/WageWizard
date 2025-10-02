export const formatIban = (iban: string | undefined | null): string => {
  if (!iban) return "-";

  return (
    iban
      .replace(/\s+/g, "")
      .match(/.{1,4}/g)
      ?.join(" ") ?? iban
  );
};
